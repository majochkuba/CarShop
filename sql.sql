CREATE TRIGGER TriggerPlatnosci
AFTER INSERT ON Payments
BEGIN
    UPDATE Orders
    SET IsPaymentCompleted = 1
    WHERE OrderId = NEW.OrderId;
END;
--
CREATE PROCEDURE ProceduraDodajUzytkownika
    @UserName TEXT,
    @PasswordHash BLOB
AS
BEGIN
    INSERT INTO Users (UserName, PasswordHash) VALUES (@UserName, @PasswordHash);
END;
--
CREATE PROCEDURE ProceduraAktualizujCeneAuta
    @CarId INTEGER,
    @NewPrice DECIMAL
AS
BEGIN
    UPDATE Cars
    SET Price = @NewPrice
    WHERE CarId = @CarId;
END;
--
CREATE FUNCTION GetTotalOrdersValue(@UserId INTEGER)
RETURNS DECIMAL
AS
BEGIN
    DECLARE @TotalValue DECIMAL;
    
    SELECT @TotalValue = SUM(Payments.Amount)
    FROM Payments
    INNER JOIN Orders ON Payments.OrderId = Orders.OrderId
    WHERE Orders.UserId = @UserId;

    RETURN @TotalValue;
END;
