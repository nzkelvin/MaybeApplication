using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaybeApplication
{
    public class Maybe<TSource>
    {
        public bool HasValue { get; set; }
        public TSource Value { get; set; }

        public Maybe()
        {
            this.HasValue = false;
        }

        public Maybe(TSource value)
        {
            if (value == null)
                this.HasValue = false;

            this.Value = value;
            this.HasValue = true;
        }

        public Maybe<TResult> Select<TResult>(Func<TSource, TResult> selector)
        {
            if (!HasValue)
                return new Maybe<TResult>();

            var result = selector.Invoke(Value);

            if (result != null)
                return new Maybe<TResult>(result);

            return new Maybe<TResult>();
        }

        public Maybe<TResult> SelectMany<TIntermedia, TResult>(Func<TSource, TIntermedia> selector,
            Func<TSource, TIntermedia, TResult> selector2)
        {
            if (!HasValue)
                return new Maybe<TResult>();

            var intermedia = selector.Invoke(Value);

            if (intermedia == null)
                return new Maybe<TResult>();

            var result = selector2.Invoke(Value, intermedia);

            if (result == null)
                return new Maybe<TResult>();

            return new Maybe<TResult>(result);
        }
    }
}
