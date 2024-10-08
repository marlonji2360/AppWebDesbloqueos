--CREATE TABLE LISTA(
--ID INT PRIMARY KEY IDENTITY(1,1),
--LISTA VARCHAR(100)
--);

CREATE PROCEDURE INSERTAR_LISTAS
@LISTA VARCHAR(100)
AS
BEGIN
	INSERT INTO LISTA VALUES (@LISTA)
END;

CREATE PROCEDURE EDITAR_LISTAS
@ID INT,
@LISTA VARCHAR(100)
AS
BEGIN
	UPDATE LISTA SET LISTA=@LISTA WHERE ID = @ID
END;

CREATE PROCEDURE ELIMINAR_LISTAS
@ID INT
AS
BEGIN
	DELETE FROM LISTA WHERE ID = @ID
END;

CREATE PROCEDURE CONSULTAR_LISTAS
AS
BEGIN
	SELECT ID, LISTA FROM LISTA
END;

CREATE PROCEDURE CONSULTAR_ID_LISTAS
@ID INT
AS
BEGIN
	SELECT ID, LISTA FROM LISTA WHERE ID = @ID
END;