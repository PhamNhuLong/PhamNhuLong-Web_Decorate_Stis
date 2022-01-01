$(document).ready(function() {
    $("#confirmpass").keyup(validate);
  });
  
  
  function validate() {
    var password1 = $("#pass").val();
    var password2 = $("#confirmpass").val();
  
      
   
      if(password1 == password2) {
        $("#message").css("color","green");  
        $("#message").text("Xác nhận mật khẩu chính xác");                       
      }
      else {
          
        $("#message").css("color","red"); 
          $("#message").text("Xác nhận mật khẩu không chính xác");         
      }
      
  }