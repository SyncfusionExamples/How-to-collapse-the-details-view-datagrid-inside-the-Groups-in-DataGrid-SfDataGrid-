# How to collapse the details view datagrid inside the Groups in DataGrid SfDataGrid

### About the sample 
This example illustrates how to collapse the details view datagrid inside the Groups in DataGrid SfDataGrid.

By default, DetailsViewDataGrid expanded state is maintain when group of DataGrid is collapsed. If we need to collapse the DetailsViewDataGrid when group of DataGrid is collapsed, this can be achieved by disabling the IsExpanded of DetailsViewDataGrid in SfDataGrid.GroupExpanding event.

```C#
this.sfDataGrid1.GroupExpanding += SfDataGrid1_GroupExpanding;

private void SfDataGrid1_GroupExpanding(object sender, Syncfusion.WinForms.DataGrid.Events.GroupChangingEventArgs e)
{
    CollapseAllNestedGrids(e.Group);
}

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
```

### Requirements to run the demo
Visual Studio 2015 and above versions
