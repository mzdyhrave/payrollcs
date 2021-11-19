﻿using System;
using HraveMzdy.Procezor.Service.Interfaces;

namespace ProcezorTests.Registry.Providers
{
    class ExampleResultConst
    {
        public const Int32 VALUE_ZERO = 0;
        public const Int32 BASIS_ZERO = 0;
        public const string DESCRIPTION_EMPTY = "result from input value";
    }
    class TimeshtWorkingResult : ExampleTermResult
    {
        public TimeshtWorkingResult(ITermTarget target, IArticleSpec spec, Int32 value, Int32 basis, string descr) : base(target, spec, value, basis, descr)
        {
        }
        public TimeshtWorkingResult(ITermTarget target, IArticleSpec spec)
        : base(target, spec, ExampleResultConst.VALUE_ZERO, ExampleResultConst.BASIS_ZERO, ExampleResultConst.DESCRIPTION_EMPTY)
        {
        }
    }

    class AmountBasisResult : ExampleTermResult
    {
        public AmountBasisResult(ITermTarget target, IArticleSpec spec, Int32 value, Int32 basis, string descr) : base(target, spec, value, basis, descr)
        {
        }
        public AmountBasisResult(ITermTarget target, IArticleSpec spec)
        : base(target, spec, ExampleResultConst.VALUE_ZERO, ExampleResultConst.BASIS_ZERO, ExampleResultConst.DESCRIPTION_EMPTY)
        {
        }
    }

    class AmountFixedResult : ExampleTermResult
    {
        public AmountFixedResult(ITermTarget target, IArticleSpec spec, Int32 value, Int32 basis, string descr) : base(target, spec, value, basis, descr)
        {
        }
        public AmountFixedResult(ITermTarget target, IArticleSpec spec)
        : base(target, spec, ExampleResultConst.VALUE_ZERO, ExampleResultConst.BASIS_ZERO, ExampleResultConst.DESCRIPTION_EMPTY)
        {
        }
    }

    class HealthInsbaseResult : ExampleTermResult
    {
        public HealthInsbaseResult(ITermTarget target, IArticleSpec spec, Int32 value, Int32 basis, string descr) : base(target, spec, value, basis, descr)
        {
        }
        public HealthInsbaseResult(ITermTarget target, IArticleSpec spec)
        : base(target, spec, ExampleResultConst.VALUE_ZERO, ExampleResultConst.BASIS_ZERO, ExampleResultConst.DESCRIPTION_EMPTY)
        {
        }
    }

    class SocialInsbaseResult : ExampleTermResult
    {
        public SocialInsbaseResult(ITermTarget target, IArticleSpec spec, Int32 value, Int32 basis, string descr) : base(target, spec, value, basis, descr)
        {
        }
        public SocialInsbaseResult(ITermTarget target, IArticleSpec spec)
        : base(target, spec, ExampleResultConst.VALUE_ZERO, ExampleResultConst.BASIS_ZERO, ExampleResultConst.DESCRIPTION_EMPTY)
        {
        }
    }

    class HealthInspaymResult : ExampleTermResult
    {
        public HealthInspaymResult(ITermTarget target, IArticleSpec spec, Int32 value, Int32 basis, string descr) : base(target, spec, value, basis, descr)
        {
        }
        public HealthInspaymResult(ITermTarget target, IArticleSpec spec)
        : base(target, spec, ExampleResultConst.VALUE_ZERO, ExampleResultConst.BASIS_ZERO, ExampleResultConst.DESCRIPTION_EMPTY)
        {
        }
    }

    class SocialInspaymResult : ExampleTermResult
    {
        public SocialInspaymResult(ITermTarget target, IArticleSpec spec, Int32 value, Int32 basis, string descr) : base(target, spec, value, basis, descr)
        {
        }
        public SocialInspaymResult(ITermTarget target, IArticleSpec spec)
        : base(target, spec, ExampleResultConst.VALUE_ZERO, ExampleResultConst.BASIS_ZERO, ExampleResultConst.DESCRIPTION_EMPTY)
        {
        }
    }

    class TaxingAdvbaseResult : ExampleTermResult
    {
        public TaxingAdvbaseResult(ITermTarget target, IArticleSpec spec, Int32 value, Int32 basis, string descr) : base(target, spec, value, basis, descr)
        {
        }
        public TaxingAdvbaseResult(ITermTarget target, IArticleSpec spec)
        : base(target, spec, ExampleResultConst.VALUE_ZERO, ExampleResultConst.BASIS_ZERO, ExampleResultConst.DESCRIPTION_EMPTY)
        {
        }
    }

    class TaxingAdvpaymResult : ExampleTermResult
    {
        public TaxingAdvpaymResult(ITermTarget target, IArticleSpec spec, Int32 value, Int32 basis, string descr) : base(target, spec, value, basis, descr)
        {
        }
        public TaxingAdvpaymResult(ITermTarget target, IArticleSpec spec)
        : base(target, spec, ExampleResultConst.VALUE_ZERO, ExampleResultConst.BASIS_ZERO, ExampleResultConst.DESCRIPTION_EMPTY)
        {
        }
    }

    class IncomeGrossResult : ExampleTermResult
    {
        public IncomeGrossResult(ITermTarget target, IArticleSpec spec, Int32 value, Int32 basis, string descr) : base(target, spec, value, basis, descr)
        {
        }
        public IncomeGrossResult(ITermTarget target, IArticleSpec spec)
        : base(target, spec, ExampleResultConst.VALUE_ZERO, ExampleResultConst.BASIS_ZERO, ExampleResultConst.DESCRIPTION_EMPTY)
        {
        }
    }

    class IncomeNettoResult : ExampleTermResult
    {
        public IncomeNettoResult(ITermTarget target, IArticleSpec spec, Int32 value, Int32 basis, string descr) : base(target, spec, value, basis, descr)
        {
        }
        public IncomeNettoResult(ITermTarget target, IArticleSpec spec)
        : base(target, spec, ExampleResultConst.VALUE_ZERO, ExampleResultConst.BASIS_ZERO, ExampleResultConst.DESCRIPTION_EMPTY)
        {
        }
    }

}
