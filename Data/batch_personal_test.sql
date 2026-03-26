SET NOCOUNT ON;

-- Inserta 10 filas de prueba
DECLARE @i INT = 1;
WHILE @i <= 10
BEGIN
    INSERT INTO dbo.personal (personal, nombre)
    VALUES ('BATCH' + RIGHT('0000' + CAST(@i AS varchar(4)), 4), 'Prueba ' + CAST(@i AS varchar(4)));
    SET @i = @i + 1;
END;

-- Fuerza 5 updates para generar eventos UPDATE
UPDATE dbo.personal
SET nombre = nombre + ' U'
WHERE personal IN ('BATCH0001','BATCH0002','BATCH0003','BATCH0004','BATCH0005');

-- Mostrar outbox para los keys de prueba
SELECT OutboxId, PersonalKey, Evento, Procesado, FechaEventoUtc
FROM dbo.PersonalAuditOutbox
WHERE PersonalKey LIKE 'BATCH%'
ORDER BY OutboxId;
