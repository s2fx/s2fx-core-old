using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using S2fx.Utility;

namespace S2fx.Tests.Utility {

    public class DependencySortTest {

        private IReadOnlyList<(string name, IEnumerable<string> deps)> CreateItems() {
            var items = new List<(string name, IEnumerable<string> deps)>() {
                ("A", new [] {"B", "C"} ),
                ("B", new string [] {} ),
                ("C", new [] {"B" } ),
                ("D", new [] {"B", "A" } ),
                ("E", new [] { "A" } ),
            };
            return items;
        }

        [Fact]
        public void TopologicalSortShouldBeOK() {
            var items = this.CreateItems();
            var sorted = items.TopologicalSort(x => x.name, x => x.deps);
            Assert.True(sorted.Select(x => x.name).SequenceEqual(new[] { "B", "C", "A", "E", "D" }));
        }

        [Fact]
        public void DependencySortShouldBeOK() {
            var items = this.CreateItems();
            var sorted = items.DependencySort(x => x.name, x => x.deps);
            Assert.True(sorted.Select(x => x.name).SequenceEqual(new[] { "D", "E", "A", "C", "B" }));
        }

    }

}
