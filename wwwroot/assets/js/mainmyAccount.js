$(document).ready(function() {
    $("#confirmpasswordtk").keyup(validate);
  });
  
  
  function validate() {
    var password1 = $("#passwordtk").val();
    var password2 = $("#confirmpasswordtk").val();
  
      
   
      if (password1 == password2) {
          
        $("#message").css("color","green");  
          $("#message").text("Xác nhận mật khẩu chính xác");       
          
      }
      else {
          
        $("#message").css("color","red"); 
          $("#message").text("Xác nhận mật khẩu không chính xác");           
      }
      
  }