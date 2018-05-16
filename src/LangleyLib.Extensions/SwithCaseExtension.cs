using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.Extensions
{
    /// <summary></summary>
    public static class SwithCaseExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="input"></param>
        /// <param name="inputSource"></param>
        /// <param name="outputSource"></param>
        /// <param name="defaultOutput"></param>
        /// <returns></returns>
        public static TOutput Switch<TOutput, TInput>(this TInput input, IEnumerable<TInput> inputSource, IEnumerable<TOutput> outputSource, TOutput defaultOutput)
        {
            IEnumerator<TInput> inputIterator = inputSource.GetEnumerator();
            IEnumerator<TOutput> outputIterator = outputSource.GetEnumerator();

            TOutput result = defaultOutput;
            while (inputIterator.MoveNext())
            {
                if (outputIterator.MoveNext())
                {
                    if (input.Equals(inputIterator.Current))
                    {
                        result = outputIterator.Current;
                        break;
                    }
                }
                else break;
            }
            return result;
        }
        #region SwithCase
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TOther"></typeparam>
        public class SwithCase<TCase, TOther>
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            /// <param name="action"></param>
            public SwithCase(TCase value, Action<TOther> action)
            {
                Value = value;
                Action = action;
            }
            /// <summary>
            /// 
            /// </summary>
            public TCase Value { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public Action<TOther> Action { get; private set; }
        }
        #endregion      

        #region Swith
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="t"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static SwithCase<TCase, TOther> Switch<TCase, TOther>(this TCase t, Action<TOther> action) where TCase : IEquatable<TCase>
        {
            return new SwithCase<TCase, TOther>(t, action);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="t"></param>
        /// <param name="selector"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static SwithCase<TCase, TOther> Switch<TInput, TCase, TOther>(this TInput t, Func<TInput, TCase> selector, Action<TOther> action) where TCase : IEquatable<TCase>
        {
            return new SwithCase<TCase, TOther>(selector(t), action);
        }
        #endregion

        #region Case
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="sc"></param>
        /// <param name="option"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static SwithCase<TCase, TOther> Case<TCase, TOther>(this SwithCase<TCase, TOther> sc, TCase option, TOther other) where TCase : IEquatable<TCase>
        {
            return Case(sc, option, other, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="sc"></param>
        /// <param name="option"></param>
        /// <param name="other"></param>
        /// <param name="bBreak"></param>
        /// <returns></returns>
        public static SwithCase<TCase, TOther> Case<TCase, TOther>(this SwithCase<TCase, TOther> sc, TCase option, TOther other, bool bBreak) where TCase : IEquatable<TCase>
        {
            return Case(sc, c => c.Equals(option), other, bBreak);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="sc"></param>
        /// <param name="predict"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static SwithCase<TCase, TOther> Case<TCase, TOther>(this SwithCase<TCase, TOther> sc, Predicate<TCase> predict, TOther other) where TCase : IEquatable<TCase>
        {
            return Case(sc, predict, other, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="sc"></param>
        /// <param name="predict"></param>
        /// <param name="other"></param>
        /// <param name="bBreak"></param>
        /// <returns></returns>
        public static SwithCase<TCase, TOther> Case<TCase, TOther>(this SwithCase<TCase, TOther> sc, Predicate<TCase> predict, TOther other, bool bBreak) where TCase : IEquatable<TCase>
        {
            if (sc == null) return null;
            if (predict(sc.Value))
            {
                sc.Action(other);
                return bBreak ? null : sc;
            }
            else return sc;
        }
        #endregion

        #region Default
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="sc"></param>
        /// <param name="other"></param>
        public static void Default<TCase, TOther>(this SwithCase<TCase, TOther> sc, TOther other)
        {
            if (sc == null) return;
            sc.Action(other);
        }
        #endregion
    }
}
