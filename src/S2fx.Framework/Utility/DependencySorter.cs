using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Utility {

    /// <summary>
    /// 依赖排序器类
    /// </summary>
    /// <typeparam name="EleType">每个元素的类型</typeparam>
    /// <typeparam name="IdType">每个元素的唯一标识类型</typeparam>
    public static class DependencySorter {

        public static void DependencySort<TEle, TId>(
            this IList<TEle> items, Func<TEle, TId> idGetter, Func<TEle, IList<TId>> dependGetter)
            where TId : IEquatable<TId> {
            if (items == null) {
                throw new ArgumentNullException(nameof(items));
            }

            if (idGetter == null) {
                throw new ArgumentNullException(nameof(idGetter));
            }

            if (dependGetter == null) {
                throw new ArgumentNullException(nameof(dependGetter));
            }

            var g = new TopologicalSorter(items.Count);
            var indexes = new Dictionary<TId, int>(items.Count);

            //add vertices
            for (var i = 0; i < items.Count; i++) {
                indexes[idGetter(items[i])] = g.AddVertex(i);
            }

            //add edges
            for (var i = 0; i < items.Count; i++) {
                if (dependGetter(items[i]) != null) {
                    for (var j = 0; j < dependGetter(items[i]).Count; j++) {
                        var id = dependGetter(items[i])[j];
                        g.AddEdge(i, indexes[id]);
                    }
                }
            }

            var sortedIndices = g.Sort();
            var sortedItems = new TEle[items.Count];
            for (var i = 0; i < items.Count; i++) {
                var sortedIndex = sortedIndices[i];
                sortedItems[i] = items[sortedIndex];
            }

            //反转
            for (var i = 0; i < items.Count; i++) {
                items[items.Count - i - 1] = sortedItems[i];
            }
        }

    }
}
