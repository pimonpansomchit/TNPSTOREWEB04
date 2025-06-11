#### Run Project 
command : dotnet run 

#### Run Gen Database 


dotnet ef dbcontext scaffold "Server=localhost;Database=WMS_TNP;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --context-dir Context  --context TMPreplenishDBContext --force --table TMPreplenishD

dotnet ef dbcontext scaffold "Server=localhost;Database=TNPWMSSYS;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --context-dir Context  --context MSTlocationDBContext --force --table MSTlocation

$(document).ready(function () {
      // Logic to handle selection (optional)
    $('#inputBarcode').on('keypress', function (e) {
    if (e.which == 13) { // Check for "Enter" key
        e.preventDefault(); // Prevent the default form submission
        $('#searchForm').submit(); // Submit the form via AJAX
    }
   
});

dotnet ef dbcontext scaffold "Server=localhost;Database=TNPWMSSYS;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --context-dir Context  --context TNPWMSSYSDBContext --force
