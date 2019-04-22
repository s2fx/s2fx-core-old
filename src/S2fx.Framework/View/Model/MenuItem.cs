using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.View.Model {

    public class MenuItem {
        public long Id { get; }
        public string Name { get; }
        public string Text { get; }
        public IEnumerable<MenuItem> Children { get; }

        public MenuItem(long id, string name, string text, IEnumerable<MenuItem> children) {
            this.Id = id;
            this.Name = name;
            this.Text = text;
            this.Children = children;
        }
    }

}
