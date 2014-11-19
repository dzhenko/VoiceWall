namespace VoiceWall.Web
{
    using Kendo.Mvc.UI;
    using Kendo.Mvc.UI.Fluent;

    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    public static class KendoHelpers
    {
        public static GridBuilder<T> FullFeaturedGrid<T>(this HtmlHelper helper, string controllerName, 
            Expression<Func<T, object>> modelIdExpression, Action<GridColumnFactory<T>> columns = null,
            object readRouteValues = null, string clientDetailTemplateId = null, string clientRowTemplate = null, string clientAltRowTemplate = null) where T : class
        {
            if (columns == null)
            {
                columns = cols =>
                {
                    cols.AutoGenerate(true);
                    cols.Command(c => c.Edit());
                    cols.Command(c => c.Destroy());
                };
            }

            var kendo = helper.Kendo()
                .Grid<T>()
                .Name("grid")
                .Columns(columns)
                .ColumnMenu()
                .Pageable(page => page.Refresh(true))
                .Sortable()
                .Groupable()
                .Filterable()
                .Editable(edit => edit.Mode(GridEditMode.PopUp))
                .DataSource(data =>
                    data
                    .Ajax()
                    .Model(m => m.Id(modelIdExpression))
                    .Read(read => read.Action("Read", controllerName, readRouteValues))
                    .Update(update => update.Action("Update", controllerName))
                    .Destroy(destroy => destroy.Action("Destroy", controllerName))
                );

            if (clientDetailTemplateId != null)
            {
                kendo = kendo.ClientDetailTemplateId(clientDetailTemplateId);
            }

            if (clientRowTemplate != null)
            {
                kendo = kendo.ClientRowTemplate(clientRowTemplate);
            }

            if (clientAltRowTemplate != null)
            {
                kendo = kendo.ClientRowTemplate(clientAltRowTemplate);
            }

            return kendo;
        }
    }
}