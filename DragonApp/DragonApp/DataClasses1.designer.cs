﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DragonApp
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="MyLoginApp")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 확장성 메서드 정의
    partial void OnCreated();
    partial void Insertweathers(weathers instance);
    partial void Updateweathers(weathers instance);
    partial void Deleteweathers(weathers instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::DragonApp.Properties.Settings.Default.MyLoginAppConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<weathers> weathers
		{
			get
			{
				return this.GetTable<weathers>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.weathers")]
	public partial class weathers : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _yymmddtt;
		
		private string _weather;
		
		private string _dust;
		
    #region 확장성 메서드 정의
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnyymmddttChanging(string value);
    partial void OnyymmddttChanged();
    partial void OnweatherChanging(string value);
    partial void OnweatherChanged();
    partial void OndustChanging(string value);
    partial void OndustChanged();
    #endregion
		
		public weathers()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_yymmddtt", DbType="NVarChar(30) NOT NULL", CanBeNull=false)]
		public string yymmddtt
		{
			get
			{
				return this._yymmddtt;
			}
			set
			{
				if ((this._yymmddtt != value))
				{
					this.OnyymmddttChanging(value);
					this.SendPropertyChanging();
					this._yymmddtt = value;
					this.SendPropertyChanged("yymmddtt");
					this.OnyymmddttChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_weather", DbType="NVarChar(30) NOT NULL", CanBeNull=false)]
		public string weather
		{
			get
			{
				return this._weather;
			}
			set
			{
				if ((this._weather != value))
				{
					this.OnweatherChanging(value);
					this.SendPropertyChanging();
					this._weather = value;
					this.SendPropertyChanged("weather");
					this.OnweatherChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dust", DbType="NVarChar(50)")]
		public string dust
		{
			get
			{
				return this._dust;
			}
			set
			{
				if ((this._dust != value))
				{
					this.OndustChanging(value);
					this.SendPropertyChanging();
					this._dust = value;
					this.SendPropertyChanged("dust");
					this.OndustChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
