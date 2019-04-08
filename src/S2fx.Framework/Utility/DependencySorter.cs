using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace S2fx.Utility {

    public static class DependencySorter {

        /// <summary>
        /// 依赖排序，是拓扑排序的逆序
        /// </summary>
        /// <typeparam name="EleType">每个元素的类型</typeparam>
        /// <typeparam name="IdType">每个元素的唯一标识类型</typeparam>
        /// <param name="source"></param>
        /// <param name="idGetter"></param>
        /// <param name="dependGetter"></param>
        /// <returns></returns>
        public static IEnumerable<TEle> DependencySort<TEle, TId>(
            this IEnumerable<TEle> source, Func<TEle, TId> idGetter, Func<TEle, IEnumerable<TId>> dependGetter)
            where TId : IEquatable<TId> {

            return source.TopologicalSort(idGetter, dependGetter).Reverse();
        }

    }
}
