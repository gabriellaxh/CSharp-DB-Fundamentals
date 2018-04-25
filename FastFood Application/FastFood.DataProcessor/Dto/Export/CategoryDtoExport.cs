using System;
using System.Collections.Generic;
using System.Text;

namespace FastFood.DataProcessor.Dto.Export
{
    public class CategoryDtoExport
    {
        public string Name { get; set; }

        public ItemDtoExport MostPopularItem { get; set; }

    }
}
