USE bdd_ces;
GO

-- 1. Crear clave maestra
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'XXX';
GO
-- 2. Crear una credencial con autenticación SASL/SSL
DROP DATABASE SCOPED CREDENTIAL eventhub_credential;
GO
CREATE DATABASE SCOPED CREDENTIAL eventhub_credential
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = 'xxx';
GO
SELECT * FROM sys.database_scoped_credentials WHERE name = 'eventhub_credential';
-- 3. Habilitar Change Event Streaming en la base de datos
EXEC sys.sp_enable_event_stream;
GO
EXEC sys.sp_drop_event_stream_group @stream_group_name = N'PagosCESGroup';
GO
-- 4. Crear grupo de secuencias de eventos apuntando a Event Hub
EXEC sys.sp_create_event_stream_group
    @stream_group_name = N'PagosCESGroup',
    @destination_type = N'AzureEventHubsAmqp',
    @destination_location = N'xxx.servicebus.windows.net/xxx',
    @destination_credential = eventhub_credential,
    @max_message_size_bytes = 1048576,
    @partition_key_scheme = N'None';
GO
-- 5. Crear grupo de tablas de eventos
EXEC sys.sp_add_object_to_event_stream_group
    N'PagosCESGroup',
    N'dbo.Pagos';
GO
EXEC sys.sp_help_change_feed_table @source_schema = 'dbo', @source_name = 'Pagos'
GO
-- 6. Validar errores
select * FROM sys.dm_change_feed_errors order by entry_time desc

