#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.Input.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetailsView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            SampleCustomization();

            this.sfDataGrid1.GroupExpanding += SfDataGrid1_GroupExpanding;
        }

        private void SfDataGrid1_GroupExpanding(object sender, Syncfusion.WinForms.DataGrid.Events.GroupChangingEventArgs e)
        {
            CollapseAllNestedGrids(e.Group);
        }


        /// <summary>
        /// Collapse All NestedGrids within a Group 
        /// </summary>
        /// <param name="group"></param>
        void CollapseAllNestedGrids(Group group)
        {
            if (group != null && group.Records != null)
            {
                foreach (var item in group.Records)
                {
                    if (item.IsGroups == false)
                        item.IsExpanded = false;
                }
            }
            if (group.Groups != null)
            {
                foreach (var item in group.Groups)
                    CollapseAllNestedGrids(item);
            }
        }

        /// <summary>
        /// Sets the sample customization settings.
        /// </summary>
        private void SampleCustomization()
        {            
            OrderInfoRepository order = new OrderInfoRepository();
            List<OrderInfo> orderDetails = order.GetOrdersDetails(100);
            this.sfDataGrid1.DataSource = orderDetails;
            this.sfDataGrid1.AllowGrouping = true;
            this.sfDataGrid1.ShowGroupDropArea = true;
            this.sfDataGrid1.HideEmptyGridViewDefinition = true;

            GridViewDefinition orderDetailsView = new GridViewDefinition();
            orderDetailsView.RelationalColumn = "OrderDetails";

            SfDataGrid childGrid = new SfDataGrid();
            childGrid.AutoGenerateColumns = false;
            childGrid.RowHeight = 21;
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalDigits = 0;
            nfi.NumberGroupSizes = new int[] { };
            childGrid.Columns.Add(new GridNumericColumn() { MappingName = "OrderID", HeaderText = "Order ID", NumberFormatInfo = nfi });
            childGrid.Columns.Add(new GridNumericColumn() { MappingName = "ProductID", HeaderText = "Product ID", NumberFormatInfo = nfi });
            childGrid.Columns.Add(new GridNumericColumn() { MappingName = "UnitPrice", HeaderText = "Unit Price", FormatMode = FormatMode.Currency });
            childGrid.Columns.Add(new GridNumericColumn() { MappingName = "Quantity" });
            childGrid.Columns.Add(new GridNumericColumn() { MappingName = "Discount", FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Percent });
            childGrid.Columns.Add(new GridTextColumn() { MappingName = "CustomerID", HeaderText = "Customer ID"});
            childGrid.Columns.Add(new GridDateTimeColumn() { MappingName = "OrderDate", HeaderText = "Order Date" });
            orderDetailsView.DataGrid = childGrid;
            this.sfDataGrid1.DetailsViewDefinitions.Add(orderDetailsView);
        }



    }
}
