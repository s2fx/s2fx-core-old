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
using S2fx.Utility;
using S2fx.View.Model.Model;
using S2fx.View.Schemas;
using S2fx.Xaml;

namespace S2fx.View.Data {

    public class ViewDataLoader : IViewDataLoader {

        readonly IHostingEnvironment _environment;
        readonly IShellFeaturesManager _shellFeaturesManager;
        readonly IDataImporter _importer;
        readonly IViewDataHarvester _harvester;
        readonly IClock _clock;
        readonly IRepository<MenuItemEntity> _menuEntityRepo;
        readonly IRepository<ViewEntity> _viewRepo;
        readonly IRepository<ActionEntity> _actionRepo;
        readonly IXamlService _xaml;

        public ILogger Logger { get; }

        public ViewDataLoader(IHostingEnvironment environment,
            IShellFeaturesManager shellFeaturesManager,
            IDataImporter importer,
            IViewDataHarvester harvester,
            IClock clock,
            IRepository<MenuItemEntity> menuEntityRepo,
            IRepository<ViewEntity> viewRepo,
            IRepository<ActionEntity> actionRepo,
            IXamlService xaml,
            ILogger<ViewDataLoader> logger) {
            _environment = environment;
            _shellFeaturesManager = shellFeaturesManager;
            _importer = importer;
            _harvester = harvester;
            _clock = clock;
            _menuEntityRepo = menuEntityRepo;
            _viewRepo = viewRepo;
            _actionRepo = actionRepo;
            _xaml = xaml;
            this.Logger = logger;
        }

        public async Task LoadAllViewsAsync() {
            this.Logger.LogInformation("Loading all views data for initialization...");

            var startedOn = _clock.UtcNow;

            var sortedFeatures = (await _shellFeaturesManager.GetEnabledFeaturesAsync())
                .DependencySort(x => x.Id, x => x.Dependencies);

            foreach (var feature in sortedFeatures) {
                await this.DoLoadViewsAsync(feature);
            }

            var elapsedTime = _clock.UtcNow - startedOn;
            this.Logger.LogInformation("All seed data loaded. Elapsed time: {0}", elapsedTime.ToString());
        }

        public async Task LoadViewsAsync(string featureId) {
            if (string.IsNullOrEmpty(featureId)) {
                throw new ArgumentNullException(nameof(featureId));
            }
            var feature = (await _shellFeaturesManager.GetEnabledFeaturesAsync()).Single(x => x.Id == featureId);
            await this.DoLoadViewsAsync(feature);
        }

        private async Task DoLoadViewsAsync(IFeatureInfo feature) {
            this.Logger.LogInformation($"Loading seed data for feature: '{feature.Id}'");

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

                default:
                    // Unknown view type
                    break;
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
            var view = await _viewRepo.FirstOrDefaultAsync(x => x.Feature == viewDef.Feature && x.Name == viewDef.Name);

            if (view == null) {
                view = new ViewEntity();
                view.Feature = viewDef.Feature;
                view.Name = viewDef.Name;
            }

            view.DisplayName = viewDef.Title;
            view.Entity = viewDef.Entity;
            view.Priority = viewDef.Priority;
            view.ViewType = viewDef.ViewType;
            view.Definition = _xaml.Save(viewDef);
            await _viewRepo.InsertOrUpdateAsync(view);
        }

        private async Task ImportActionDefinitionAsync(AbstractActionDefinition actionDef) {
            var action = await _actionRepo.FirstOrDefaultAsync(x => x.Feature == actionDef.Feature && x.Name == actionDef.Name);

            if (action == null) {
                action = new ActionEntity();
                action.Feature = actionDef.Feature;
                action.Name = actionDef.Name;
            }

            action.DisplayName = actionDef.Text;
            action.Entity = actionDef.Entity;
            action.ActionType = actionDef.ActionType;
            action.Definition = _xaml.Save(actionDef);
            await _actionRepo.InsertOrUpdateAsync(action);
        }

    }

}
