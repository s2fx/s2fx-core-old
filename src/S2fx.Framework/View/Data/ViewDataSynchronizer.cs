using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Extensions.Features;
using OrchardCore.Environment.Shell;
using OrchardCore.Modules;
using S2fx.Data;
using S2fx.Data.Importing;
using S2fx.Model;
using S2fx.Utility;
using S2fx.View.Model.Model;
using S2fx.View.Schemas;
using S2fx.Xaml;

namespace S2fx.View.Data {

    public class ViewDataSynchronizer : IViewDataSynchronizer {

        readonly IWebHostEnvironment _environment;
        readonly IShellFeaturesManager _shellFeaturesManager;
        readonly IDataImporter _importer;
        readonly IViewHarvester _harvester;
        readonly IClock _clock;
        readonly IRepository<MenuItemEntity> _menuEntityRepo;
        readonly IRepository<ViewEntity> _viewRepo;
        readonly IRepository<ActionEntity> _actionRepo;
        readonly IRepository<ViewFragmentEntity> _viewFragmentRepo;
        readonly IXamlService _xaml;

        public ILogger Logger { get; }

        public ViewDataSynchronizer(IWebHostEnvironment environment,
            IShellFeaturesManager shellFeaturesManager,
            IDataImporter importer,
            IViewHarvester harvester,
            IClock clock,
            IRepository<MenuItemEntity> menuEntityRepo,
            IRepository<ViewEntity> viewRepo,
            IRepository<ActionEntity> actionRepo,
            IRepository<ViewFragmentEntity> viewFragmentRepo,
            IXamlService xaml,
            ILogger<ViewDataSynchronizer> logger) {
            _environment = environment;
            _shellFeaturesManager = shellFeaturesManager;
            _importer = importer;
            _harvester = harvester;
            _clock = clock;
            _menuEntityRepo = menuEntityRepo;
            _viewRepo = viewRepo;
            _actionRepo = actionRepo;
            _viewFragmentRepo = viewFragmentRepo;
            _xaml = xaml;
            this.Logger = logger;
        }

        public async Task SynchronizeAllViewsAsync() {
            this.Logger.LogInformation("Synchronizing all views for initialization...");

            var startedOn = _clock.UtcNow;

            var sortedFeatures = (await _shellFeaturesManager.GetEnabledFeaturesAsync());

            foreach (var feature in sortedFeatures) {
                await this.InternalSynchronizeViewsAsync(feature);
            }

            var elapsedTime = _clock.UtcNow - startedOn;
            this.Logger.LogInformation("All views has been synchronized. Elapsed time: {0}", elapsedTime.ToString());
        }

        public async Task SynchronizeViewsAsync(string featureId) {
            if (string.IsNullOrEmpty(featureId)) {
                throw new ArgumentNullException(nameof(featureId));
            }
            var feature = (await _shellFeaturesManager.GetEnabledFeaturesAsync()).Single(x => x.Id == featureId);
            await this.InternalSynchronizeViewsAsync(feature);
        }

        private async Task InternalSynchronizeViewsAsync(IFeatureInfo feature) {
            this.Logger.LogInformation($"Synchronizing view data for feature: '{feature.Id}'");

            var viewDefinitions = await _harvester.HarvestAsync(feature);

            foreach (var viewDefinition in viewDefinitions) {
                if (string.IsNullOrEmpty(viewDefinition.Feature) || viewDefinition.Feature == feature.Id) {
                    await this.ImportViewDefinitionEntryAsync(viewDefinition);
                }
            }

        }

        private async Task ImportViewDefinitionEntryAsync(IViewDefinition vd) {

            switch (vd) {
                case MenuItem menuItem:
                    await this.ImportMenuItemDefinition(menuItem);
                    break;

                case AbstractEntityViewDefinition entityView:
                    await this.ImportViewDefinitionAsync(entityView);
                    break;

                case AbstractActionDefinition action:
                    await this.ImportActionDefinitionAsync(action);
                    break;

                case ViewFragment fragment:
                    await this.ImportViewFragmentDefinitionAsync(fragment);
                    break;

                default:
                    throw new NotSupportedException($"Unsupported view definition '{vd.GetType().Name}' to import");
            }

        }

        private async Task ImportMenuItemDefinition(MenuItem menuDef) {
            var menu = await _menuEntityRepo.FirstOrDefaultAsync(x => x.Feature == menuDef.Feature && x.Name == menuDef.Name);
            var parent = menuDef.Parent != null ? await _menuEntityRepo.SingleAsync(x => x.Name == menuDef.Parent) : null;
            var action = menuDef.Action != null ? await _actionRepo.SingleAsync(x => x.Name == menuDef.Action) : null;

            if (menu == null) {
                menu = new MenuItemEntity();
                menu.Feature = menuDef.Feature;
                menu.Name = menuDef.Name;
            }

            menu.Action = action;
            menu.Order = menuDef.Order;
            menu.Text = menuDef.Text;
            menu.BackgroundColor = menuDef.BackgroundColor;
            menu.Icon = menuDef.Icon;
            menu._Parent = parent;

            await _menuEntityRepo.InsertOrUpdateAsync(menu);
        }

        private async Task ImportViewDefinitionAsync(AbstractEntityViewDefinition viewDef) {
            await this.InternalImportDefinitionAsync(viewDef, _viewRepo, view => {
                view.DisplayName = viewDef.Title;
                view.Entity = viewDef.Entity;
                view.Priority = viewDef.Priority;
                view.ViewType = viewDef.ViewType;
            });
        }

        private async Task ImportActionDefinitionAsync(AbstractActionDefinition actionDef) {
            await this.InternalImportDefinitionAsync(actionDef, _actionRepo, action => {
                action.DisplayName = actionDef.Text;
                action.Entity = actionDef.Entity;
                action.ActionType = actionDef.ActionType;
                action.Priority = actionDef.Priority;
                if (actionDef is ViewAction va) {
                    action.CanBeHome = va.CanBeHome;
                }
            });
        }

        private async Task ImportViewFragmentDefinitionAsync(ViewFragment fragmentDef) {
            var extendsView = fragmentDef.Extends != null ? await _viewRepo.SingleAsync(x => x.Name == fragmentDef.Extends) : null;
            await this.InternalImportDefinitionAsync(fragmentDef, _viewFragmentRepo, fragment => {
                fragment.DisplayName = fragmentDef.Title;
                fragment.Priority = fragmentDef.Priority;
                fragment.Extends = extendsView;
            });
        }

        private async Task InternalImportDefinitionAsync<TEntity, TDefinition>(TDefinition definition, IRepository<TEntity> repo, Action<TEntity> config)
            where TEntity : AbstractDefinitionEntity, new()
            where TDefinition : IViewDefinition {
            var record = await repo.FirstOrDefaultAsync(x => x.Feature == definition.Feature && x.Name == definition.Name);

            if (record == null) {
                record = new TEntity();
                record.Feature = definition.Feature;
                record.Name = definition.Name;
                record.Definition = _xaml.Save(definition);
            }
            config(record);
            await repo.InsertOrUpdateAsync(record);
        }

    }

}
