﻿using Macaw.Umbraco.Foundation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Dynamics;
using Umbraco.Core.Models.PublishedContent;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
    public class RelatedLinks : BaseConverter
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return Constants.PropertyEditors.RelatedLinksAlias.Equals(propertyType.PropertyEditorAlias);
        }

        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            var ret = new List<UrlModel>(); //return value
            var arr = Newtonsoft.Json.JsonConvert.DeserializeObject(source.ToString()) as Newtonsoft.Json.Linq.JArray;

            foreach (var item in arr)
            {
                int id;
                if (int.TryParse(item["link"].ToString(), out id)) //internal
                {
                    ret.Add(new UrlModel
                    {
                        Title = item["title"].ToString(),
                        Url = Repository.FriendlyUrl(id)
                    });
                }
                else //external link
                {
                    ret.Add(new UrlModel
                    {
                        Title = item["title"].ToString(),
                        Url = item["link"].ToString()
                    });
                }
            }

            return ret;
        }
    }
}