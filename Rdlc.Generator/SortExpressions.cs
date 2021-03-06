﻿namespace Rdlc.Generator
{
    public class SortExpressions : CollectionOf<SortExpression>, IElement
    {
        public SortExpressions(SortExpression sortExpression)
            : base(sortExpression)
        {            
        }

        protected sealed override string GetRdlName()
        {
            return typeof(SortExpressions).GetShortName();
        }
    }
}
