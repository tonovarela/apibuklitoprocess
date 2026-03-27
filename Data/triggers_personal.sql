

-- 1) Create outbox table
CREATE TABLE dbo.PersonalAuditOutbox (
    OutboxId           bigint IDENTITY(1,1) PRIMARY KEY,
    PersonalKey        varchar(20) NOT NULL,
    Evento             varchar(20) NOT NULL,
    FechaEventoUtc     datetime2(3) NOT NULL CONSTRAINT DF_PAO_Fecha DEFAULT (sysutcdatetime()),
    Procesado          bit NOT NULL CONSTRAINT DF_PAO_Procesado DEFAULT (0),
    FechaProcesadoUtc  datetime2(3) NULL,
    Intentos           int NOT NULL CONSTRAINT DF_PAO_Intentos DEFAULT (0),
    ErrorUltimo        nvarchar(4000) NULL
);

CREATE INDEX IX_PAO_Pendientes
ON dbo.PersonalAuditOutbox (Procesado, OutboxId)
INCLUDE (PersonalKey, Evento, FechaEventoUtc);
GO

-- 2) Trigger on dbo.personal
CREATE OR ALTER TRIGGER dbo.trg_personal_audit_outbox
ON dbo.personal
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- INSERT (new rows)
    INSERT INTO dbo.PersonalAuditOutbox (PersonalKey, Evento)
    SELECT i.personal, 'INSERT'
    FROM inserted i
    LEFT JOIN deleted d ON d.personal = i.personal
    WHERE d.personal IS NULL;

    -- UPDATE (existing rows modified)
    INSERT INTO dbo.PersonalAuditOutbox (PersonalKey, Evento)
    SELECT i.personal, 'UPDATE'
    FROM inserted i
    INNER JOIN deleted d ON d.personal = i.personal;
END;
GO
