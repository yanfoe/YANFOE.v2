namespace YANFOE.UI.UserControls.CommonControls
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;

    using DevExpress.XtraEditors;

    using YANFOE.Factories.Internal;

    public partial class ListEditUserControl : XtraUserControl
    {


        public ListEditUserControl()
        {
            InitializeComponent();
            gridControl.Enabled = false;
        }

        public BindingList<StringObject> StringBindingList;

        private BindingList<string> origDataSource;

        public void SetDataSource(BindingList<string> datasource)
        {
            this.StringBindingList = new BindingList<StringObject>();
            this.origDataSource = datasource;

            var items = from s in datasource orderby s select s;

            foreach (var s in items)
            {
                if (!StringBindingList.Any(item => item.Text == s))
                {
                    StringBindingList.Add(new StringObject { Text = s });
                }
            }

            this.SetDataSourceList();

            gridControl.DataSource = StringBindingList;
            gridControl.Enabled = true;

            StringBindingList.AddingNew += this.StringBindingList_AddingNew;
            StringBindingList.ListChanged += this.StringBindingList_ListChanged;
        }

        private void SetDataSourceList()
        {
            origDataSource.Clear();
            foreach (var s in this.StringBindingList)
            {
                origDataSource.Add(s.Text);
            }
        }

        void StringBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            SetDataSourceList();
        }

        void StringBindingList_AddingNew(object sender, AddingNewEventArgs e)
        {
            var obj = e.NewObject as StringObject;
            origDataSource.Add(obj.Text);
        }

        public class StringObject : INotifyPropertyChanged
        {
            private string text;

            public string Text
            {
                get
                {
                    return this.text;
                }
                set
                {
                    this.text = value;
                    this.OnPropertyChanged("Text");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string name)
            {
                if (DatabaseIOFactory.AppLoading)
                {
                    return;
                }

                PropertyChangedEventHandler handler = this.PropertyChanged;

                if (handler != null)
                {
                    try
                    {
                        handler(this, new PropertyChangedEventArgs(name));
                    }
                    catch (Exception ex)
                    {
                        Debug.Write(ex);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (var rowIndex in gridView1.GetSelectedRows())
            {
                var row = gridView1.GetRow(rowIndex) as StringObject;
                StringBindingList.Remove(row);
            }
        }

    }
}
