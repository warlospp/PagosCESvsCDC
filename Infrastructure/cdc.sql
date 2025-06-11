USE bdd_cdc;
GO
-- 1. Habilitar CDC en la base de datos (si no está habilitado)
EXEC sys.sp_cdc_enable_db;
GO
-- 2. Habilitar CDC en la tabla pagos_1
EXEC sys.sp_cdc_enable_table  
    @source_schema = N'dbo',  -- Cambiar si el esquema es distinto
    @source_name = N'Pagos',  
    @role_name = NULL;  -- Puedes especificar un rol si quieres controlar permisos
GO