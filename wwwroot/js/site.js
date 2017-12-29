/* TheWorld JS File */

// self-executing anonymous function
// by wrapping the func. in Parenthesis () it becomes an expression that returns a function
// following up this expression with more parenthesis '();' executes the returned function

(function () {


   
    // all of the following commented code was just examples explaining the benefits of JQERY over 
    // javascript


    //var ele = $("#username");

    //ele.text("Jake Nesom");

    //var main = $("#main");

    //main.on("mouseenter", function () {
    //    main.style.backgroundColor = "#888";
    //});

    //main.on("mouseleave", function () {
    //    main.style.backgroundColor = "";
    //});

   

    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function () {
    //    //wrapping 'this pointer'
    //    // 'this' represents the object the function is related to
    //    // in this case, the object is the 'ul.menu li a' tag being clicked

    //    var me = $(this);

    //    alert(me.text());
    //});

    var $sidebarAndWrapper = $("#sidebar,#wrapper"); // returns a 'wrapped set' of DOM elements

    $("#sidebarToggle").on("click", function () {
        console.log("test");
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $(this).text("Show Sidebar");
        } else {
            $(this).text("Hide Sidebar");
        }
    });
    
})();





