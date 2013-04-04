// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ThreadedBindingList.cs">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <license>
//   This software is licensed under a Creative Commons License
//   Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0)
//   http://creativecommons.org/licenses/by-nc-sa/3.0/
//   See this page: http://www.yanfoe.com/license
//   For any reuse or distribution, you must make clear to others the
//   license terms of this work.
// </license>
// <summary>
//   The threaded binding list.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.UI
{
    #region Required Namespaces

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Threading;

    #endregion

    /// <summary>
    /// The threaded binding list.
    /// </summary>
    /// <typeparam name="T">
    /// ThreadedBindingList container type 
    /// </typeparam>
    [Serializable]
    public class ThreadedBindingList<T> : BindingList<T>
    {
        #region Fields

        /// <summary>
        ///   The comparers.
        /// </summary>
        [NonSerialized]
        private readonly Dictionary<Type, PropertyComparer<T>> comparers;

        /// <summary>
        ///   The context.
        /// </summary>
        [NonSerialized]
        private readonly SynchronizationContext ctx = SynchronizationContext.Current;

        /// <summary>
        ///   The is sorted.
        /// </summary>
        [NonSerialized]
        private bool isSorted;

        /// <summary>
        ///   The list sort direction.
        /// </summary>
        [NonSerialized]
        private ListSortDirection listSortDirection;

        /// <summary>
        ///   The property descriptor.
        /// </summary>
        [NonSerialized]
        private PropertyDescriptor propertyDescriptor;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="ThreadedBindingList{T}" /> class.
        /// </summary>
        public ThreadedBindingList()
            : base(new List<T>())
        {
            this.comparers = new Dictionary<Type, PropertyComparer<T>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadedBindingList{T}"/> class.
        /// </summary>
        /// <param name="enumeration">
        /// The enumeration. 
        /// </param>
        public ThreadedBindingList(IEnumerable<T> enumeration)
            : base(new List<T>(enumeration))
        {
            this.comparers = new Dictionary<Type, PropertyComparer<T>>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets a value indicating whether is sorted core.
        /// </summary>
        protected override bool IsSortedCore
        {
            get
            {
                return this.isSorted;
            }
        }

        /// <summary>
        ///   Gets the sort direction core.
        /// </summary>
        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return this.listSortDirection;
            }
        }

        /// <summary>
        ///   Gets the sort property core.
        /// </summary>
        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return this.propertyDescriptor;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether supports searching core.
        /// </summary>
        protected override bool SupportsSearchingCore
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether supports sorting core.
        /// </summary>
        protected override bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The apply sort core.
        /// </summary>
        /// <param name="prop">
        /// The prop. 
        /// </param>
        /// <param name="direction">
        /// The direction. 
        /// </param>
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            Type propertyType = prop.PropertyType;
            PropertyComparer<T> comparer;
            if (!this.comparers.TryGetValue(propertyType, out comparer))
            {
                comparer = new PropertyComparer<T>(prop, direction);
                this.comparers.Add(propertyType, comparer);
            }
        }

        /// <summary>
        /// The find core.
        /// </summary>
        /// <param name="prop">
        /// The prop. 
        /// </param>
        /// <param name="key">
        /// The key. 
        /// </param>
        /// <returns>
        /// The <see cref="int"/> . 
        /// </returns>
        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            int count = this.Count;
            for (int i = 0; i < count; ++i)
            {
                T element = this[i];
                var value = prop.GetValue(element);
                if (value != null && value.Equals(key))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// The on adding new.
        /// </summary>
        /// <param name="e">
        /// The e. 
        /// </param>
        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            if (this.ctx == null)
            {
                this.BaseAddingNew(e);
            }
            else
            {
                this.ctx.Send(delegate { this.BaseAddingNew(e); }, null);
            }
        }

        /// <summary>
        /// The on list changed.
        /// </summary>
        /// <param name="e">
        /// The e. 
        /// </param>
        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (this.ctx == null)
            {
                this.BaseListChanged(e);
            }
            else
            {
                this.ctx.Send(delegate { this.BaseListChanged(e); }, null);
            }
        }

        /// <summary>
        ///   The remove sort core.
        /// </summary>
        protected override void RemoveSortCore()
        {
            this.isSorted = false;
            this.propertyDescriptor = base.SortPropertyCore;
            this.listSortDirection = base.SortDirectionCore;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// The base adding new.
        /// </summary>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void BaseAddingNew(AddingNewEventArgs e)
        {
            base.OnAddingNew(e);
        }

        /// <summary>
        /// The base list changed.
        /// </summary>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void BaseListChanged(ListChangedEventArgs e)
        {
            base.OnListChanged(e);
        }

        #endregion
    }

    /// <summary>
    /// The property comparer.
    /// </summary>
    /// <typeparam name="T">
    /// PropertyComparer Type 
    /// </typeparam>
    public class PropertyComparer<T> : IComparer<T>
    {
        #region Fields

        /// <summary>
        ///   The comparer.
        /// </summary>
        private readonly IComparer comparer;

        /// <summary>
        ///   The property descriptor.
        /// </summary>
        private PropertyDescriptor propertyDescriptor;

        /// <summary>
        ///   The reverse.
        /// </summary>
        private int reverse;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyComparer{T}"/> class.
        /// </summary>
        /// <param name="property">
        /// The property. 
        /// </param>
        /// <param name="direction">
        /// The direction. 
        /// </param>
        public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
        {
            this.propertyDescriptor = property;
            Type comparerForPropertyType = typeof(Comparer<>).MakeGenericType(property.PropertyType);
            this.comparer =
                (IComparer)
                comparerForPropertyType.InvokeMember(
                    "Default", BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.Public, null, null, null);
            this.SetListSortDirection(direction);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The <see cref="int"/> . 
        /// </returns>
        public int Compare(T x, T y)
        {
            return this.reverse
                   * this.comparer.Compare(this.propertyDescriptor.GetValue(x), this.propertyDescriptor.GetValue(y));
        }

        /// <summary>
        /// The set property and direction.
        /// </summary>
        /// <param name="descriptor">
        /// The descriptor. 
        /// </param>
        /// <param name="direction">
        /// The direction. 
        /// </param>
        public void SetPropertyAndDirection(PropertyDescriptor descriptor, ListSortDirection direction)
        {
            this.SetPropertyDescriptor(descriptor);
            this.SetListSortDirection(direction);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The set list sort direction.
        /// </summary>
        /// <param name="direction">
        /// The direction. 
        /// </param>
        private void SetListSortDirection(ListSortDirection direction)
        {
            this.reverse = direction == ListSortDirection.Ascending ? 1 : -1;
        }

        /// <summary>
        /// The set property descriptor.
        /// </summary>
        /// <param name="descriptor">
        /// The descriptor. 
        /// </param>
        private void SetPropertyDescriptor(PropertyDescriptor descriptor)
        {
            this.propertyDescriptor = descriptor;
        }

        #endregion
    }
}