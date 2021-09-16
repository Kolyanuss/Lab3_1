-- =============================================
-- Description:	stored procedure to get all records from Employees_Skills table by employee id
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllEmployeeSkillsByEmployeeId]
	-- Add the parameters for the stored procedure here
	@P_Id nvarchar = null
AS
BEGIN
	SET NOCOUNT ON;
	
	Declare @V_sql as nvarchar(MAX) = null
	if (@P_Id is not null)
		select @V_sql = 'select * from Employees_Skills where Employee_Id = ' + @P_Id + ';'

	if(@V_sql is not null)
		exec(@V_sql)
	else
		select -1;
END