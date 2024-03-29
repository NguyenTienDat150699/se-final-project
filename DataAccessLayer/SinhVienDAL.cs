﻿using Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SinhVienDAL : MSAccessDatabase
    {
        public SinhVienDAL(OleDbConnection connection) : base(connection) { }
        public SinhVienDAL(string connectionString) : base(connectionString) { }

        public void CreateItem(SinhVien sinhVien)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "INSERT INTO SINH_VIEN (MaSo, HoTen, NgaySinh, GioiTinh, DiaChi, QueQuan, DoiTuong, NganhHoc) " +
                    "VALUES (@maso, @hoten, @ngaysinh, @gioitinh, @diachi, @quequan, @doituong, @nganhhoc)",
                    connection);
                command.Parameters.Add("@maso", OleDbType.Numeric).Value = sinhVien.MaSo;
                command.Parameters.Add("@hoten", OleDbType.BSTR).Value = sinhVien.HoTen;
                command.Parameters.Add("@ngaysinh", OleDbType.Date).Value = sinhVien.NgaySinh;
                command.Parameters.Add("@gioitinh", OleDbType.Boolean).Value = sinhVien.GioiTinh;
                command.Parameters.Add("@diachi", OleDbType.BSTR).Value = sinhVien.DiaChi;
                command.Parameters.Add("@quequan", OleDbType.Numeric).Value = sinhVien.QueQuan;
                command.Parameters.Add("@doituong", OleDbType.Numeric).Value = sinhVien.DoiTuong;
                command.Parameters.Add("@nganhhoc", OleDbType.Numeric).Value = sinhVien.NganhHoc;

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool IsMaSoExisted(int maSo)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT COUNT(MaSo) FROM SINH_VIEN WHERE MaSo=@maso", connection);
                command.Parameters.Add("@maso", OleDbType.Numeric).Value = maSo;
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(command);
                oleDbDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return int.Parse(dataTable.Rows[0][0].ToString()) == 0 ? false : true;
        }

        public List<SinhVien> ReadAllItems()
        {
            List<SinhVien> sinhViens = new List<SinhVien>();
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT * FROM SINH_VIEN ORDER BY MaSo ASC", connection);
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(command);
                oleDbDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            foreach (DataRow row in dataTable.Rows)
            {
                SinhVien sinhVien = new SinhVien();
                sinhVien.MaSo = int.Parse(row["MaSo"].ToString());
                sinhVien.HoTen = row["HoTen"].ToString();
                sinhVien.NgaySinh = DateTime.Parse(row["NgaySinh"].ToString());
                sinhVien.GioiTinh = bool.Parse(row["GioiTinh"].ToString());
                sinhVien.DiaChi = row["DiaChi"].ToString();
                sinhVien.QueQuan = int.Parse(row["QueQuan"].ToString());
                sinhVien.DoiTuong = int.Parse(row["DoiTuong"].ToString());
                sinhVien.NganhHoc = int.Parse(row["NganhHoc"].ToString());
                sinhViens.Add(sinhVien);
            }
            return sinhViens;
        }
    }
}
