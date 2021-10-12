﻿using System;
using System.Collections.Generic;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Procezor.Service.Errors;
using HraveMzdy.Procezor.Service.Interfaces;
using ResultMonad;

namespace HraveMzdy.Procezor.Registry
{
    using ResultFunc = Func<ITermTarget, IPeriod, IBundleProps, IList<Result<ITermResult, ITermResultError>>, IEnumerable<Result<ITermResult, ITermResultError>>>;
    interface ITermCalcul : ITermSymbol
    {
        ITermTarget Target { get; }
        ResultFunc ResultDelegate { get; }
        IEnumerable<Result<ITermResult, ITermResultError>> GetResults<EA, EC>(IPeriod period, IBundleProps propsLegal, IList<Result<ITermResult, ITermResultError>> results)
            where EA : struct, IComparable where EC : struct, IComparable;
    }
}
