using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.View.Model {

    public class MenuItem {
        public long Id { get; }
        public string Name { get; }
        public string Text { get; }
        public int Order { get; }
        public long? ParentId { get; }
        public IEnumerable<MenuItem> Children { get; }
        public string Icon { get; }
        public string BackgroundColor { get; }
        public long? ActionId { get; }
        public string ActionName { get; }
        public int Depth { get; }

        public MenuItem(
            long id, string name, string text, int order, long? parentId, IEnumerable<MenuItem> children,
            long? actionId, string actionName, int depth = 0, string icon = null, string backgroundColor = null) {

            this.Id = id;
            this.Name = name;
            this.Text = text;
            this.Order = order;
            this.Children = children;
            this.ActionName = actionName;
            this.Depth = depth;
            this.Icon = icon;
            this.BackgroundColor = backgroundColor;
        }
    }

}
