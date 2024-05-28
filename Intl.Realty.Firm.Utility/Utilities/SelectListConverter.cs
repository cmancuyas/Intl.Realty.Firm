﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Intl.Realty.Firm.Utility.Utilities
{
    public static class SelectListConverter
    {
        public static IEnumerable<SelectListItem> CreateSelectList<T>(IList<T> entities,
                                                    Func<T, object> funcToGetValue,
                                                    Func<T, object> funcToGetText)
        {
            return entities
                    .Select(x => new SelectListItem
                    {
                        Value = funcToGetValue(x).ToString(),
                        Text = funcToGetText(x).ToString(),
                    });
        }
    }
}
