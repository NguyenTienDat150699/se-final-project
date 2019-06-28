# Đồ án cuối kỳ - Nhập Môn Công Nghệ Phần Mềm

## Hướng dẫn Run Project

1. __Đổi đường dẫn đến Database__

Chỉnh sửa connectionString trong tập tin __App.config__

```
<connectionStrings>
        <add name="Database" connectionString="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=<đường dẫn đến nơi chứa solution>/se-final-project\DataAccessLayer\Database.mdb;Persist Security Info=True"/>
</connectionStrings>

```

2. __Set startup View Project__
