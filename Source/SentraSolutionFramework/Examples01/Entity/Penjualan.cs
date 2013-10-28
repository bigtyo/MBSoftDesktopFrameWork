using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using DevExpress.Data;
using System.Collections;

namespace Examples01.Entity
{
    public enum StatusJual
    {
        BelumLunas,
        Lunas,
        Batal
    }

    public class ItemPenjualan : ChildEntity<Penjualan>
    {
        string _KodeItem;
        string _NamaItem;
        decimal _Jumlah;
        decimal _HargaSatuan;

        [CounterKey]
        public int Ctr;
        
        [DataTypeVarChar(20)]
        public string KodeItem
        {
            get { return _KodeItem; }
            set
            {
                _KodeItem = value;
                _NamaItem = (string)FindValue<Item>(
                    "NamaItem", "KodeItem=@KodeItem",
                    new FieldParam("KodeItem", value));
                DataChanged();
            }
        }
        public string NamaItem
        {
            get { return _NamaItem; }
            set
            {
                _NamaItem = value;
                _KodeItem = (string)FindValue<Item>(
                    "KodeItem", "NamaItem=" + 
                    FormatSqlValue(value));
                DataChanged();
            }
        }
        [DataTypeDecimal(Default = 1)]
        public decimal Jumlah
        {
            get { return _Jumlah; }
            set
            {
                _Jumlah = value;
                DataChanged();
            }
        }
        [DataTypeDecimal]
        public decimal HargaSatuan
        {
            get { return _HargaSatuan; }
            set
            {
                _HargaSatuan = value;
                DataChanged();
            }
        }
        [DataTypeDecimal]
        public decimal Total
        {
            get { return _Jumlah * _HargaSatuan; }
            set
            {
                if (_Jumlah == 0) _Jumlah = 1;
                _HargaSatuan = value / _Jumlah;
                DataChanged();
            }
        }
    }

    public class Penjualan : ParentEntity
    {
        decimal _Total;
        decimal _Diskon;
        decimal _TotalDibayar;
        string _NoPelanggan;
        string _NamaPelanggan;

        [AutoNumberKey("PNJYYMM-CCCC", "CCCC", "TglJual")]
        public string NoPenjualan;

        [DataTypeVarChar(20)]
        public string NoPelanggan
        {
            get { return _NoPelanggan; }
            set
            {
                _NoPelanggan = value;
                _NamaPelanggan = (string)FindValue<Pelanggan>(
                    "NamaPelanggan", "NoPelanggan=" + 
                    FormatSqlValue(value));
                DataChanged();
            }
        }
        public string NamaPelanggan
        {
            get { return _NamaPelanggan; }
            set
            {
                _NamaPelanggan = value;
                _NoPelanggan = (string)FindValue<Pelanggan>(
                    "NoPelanggan", "NamaPelanggan=" + 
                    FormatSqlValue(value));
                DataChanged();
            }
        }
        [DataTypeDateTime]
        public DateTime TglJual;
        [DataTypeInteger]
        public StatusJual Status;
        [DataTypeDecimal]
        public decimal Total
        {
            get { return _Total; }
            set
            {
                _Total = value;
                DataChanged();
            }
        }
        [DataTypeDecimal]
        public decimal Diskon
        {
            get { return _Diskon; }
            set
            {
                _Diskon = value;
                DataChanged();
            }
        }
        [DataTypeDecimal]
        public decimal TotalAkhir
        {
            get { return _Total - _Diskon; }
            set
            {
                _Diskon = _Total - value;
                DataChanged();
            }
        }
        [DataTypeDecimal]
        public decimal TotalDibayar
        {
            get { return _TotalDibayar; }
            set
            {
                _TotalDibayar = value;
                DataChanged();
            }
        }
        [DataTypeTimeStamp]
        public DateTime TglJamBuat;
        [DataTypeVarChar(50)]
        public string Keterangan;

        public EntityCollection<ItemPenjualan> ItemPenjualan;

        protected override void onChildDataChanged(string ChildName)
        {
            _Total = ItemPenjualan.Sum("Total");
            DataChanged();
        }
    }
}
