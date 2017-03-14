﻿namespace Rdlc.Generator
{
    public class DataSets : CollectionOf<DataSet>, IElement
    {
        protected sealed override string GetRdlName()
        {
            return typeof(DataSets).GetShortName();
        }
    }
}
