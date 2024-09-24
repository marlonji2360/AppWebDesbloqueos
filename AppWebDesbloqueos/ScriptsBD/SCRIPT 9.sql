create procedure OBTENERDESBLOQUEOSPORFECHA
	@FechaInicio DATE,
	@FechaFin DATE

AS

BEGIN
	select * from dbo.DESBLOQUEOS
	where FECHA_CORREO between @FechaInicio AND @FechaFin;
END

