//Coming from https://stackoverflow.com/questions/4106862/how-to-sort-depended-objects-by-dependency
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace S2fx.Utility {

    /// <summary>
    /// 拓扑排序类
    /// </summary>
    public static class TopologicalSorter {

        public static IEnumerable<T> TopogicalDFSSort<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> deps) {
            var yielded = new HashSet<T>();
            var visited = new HashSet<T>();
            var stack = new Stack<(T, IEnumerator<T>)>();

            foreach (T t in source) {
                stack.Clear();
                if (visited.Add(t))
                    stack.Push((t, deps(t).GetEnumerator()));

                while (stack.Count > 0) {
                    var p = stack.Peek();
                    bool depPushed = false;
                    while (p.Item2.MoveNext()) {
                        var curr = p.Item2.Current;
                        if (visited.Add(curr)) {
                            stack.Push((curr, deps(curr).GetEnumerator()));
                            depPushed = true;
                            break;
                        }
                        else if (!yielded.Contains(curr)) {
                            throw new NotSupportedException("cycle");
                        }
                    }

                    if (!depPushed) {
                        p = stack.Pop();
                        if (!yielded.Add(p.Item1)) {
                            throw new InvalidOperationException("bug");
                        }
                        yield return p.Item1;
                    }
                }
            }
        }

        public static IEnumerable<T> TopologicalBFSSort<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> dependencies) {
            var yielded = new HashSet<T>();
            var visited = new HashSet<T>();
            var stack = new Stack<(T, bool)>(source.Select(s => (s, false))); // bool signals Add to sorted

            while (stack.Count > 0) {
                var item = stack.Pop();
                if (!item.Item2) {
                    if (visited.Add(item.Item1)) {
                        stack.Push((item.Item1, true)); // To be added after processing the dependencies
                        foreach (var dep in dependencies(item.Item1))
                            stack.Push((dep, false));
                    }
                    else if (!yielded.Contains(item.Item1)) {
                        throw new NotSupportedException("cyclic");
                    }
                }
                else {
                    if (!yielded.Add(item.Item1))
                        throw new InvalidOperationException("bug");
                    yield return item.Item1;
                }
            }
        }

        /// <summary>
        /// 广度优先搜索的拓扑排序
        /// </summary>
        /// <typeparam name="TEle"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="source"></param>
        /// <param name="idGetter"></param>
        /// <param name="dependGetter"></param>
        /// <returns></returns>
        public static IEnumerable<TEle> TopologicalSort<TEle, TId>(
            this IEnumerable<TEle> source, Func<TEle, TId> idGetter, Func<TEle, IEnumerable<TId>> dependGetter)
            where TId : IEquatable<TId> {

            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }

            if (idGetter == null) {
                throw new ArgumentNullException(nameof(idGetter));
            }

            var map = source.ToDictionary(idGetter);
            var topoSortedIds = map.Keys.TopologicalBFSSort(x => dependGetter(map[x]));
            return topoSortedIds.Select(id => map[id]);
        }

    }
}
