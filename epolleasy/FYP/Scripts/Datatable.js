//$(document).ready(function() {
//    var t = $('#example').DataTable();
//    var counter = 1;
 
//    $('#addRow').on( 'click', function () {
//        t.row.add( [
//            counter +'.1',
//            counter +'.2',
//            counter +'.3',
//            counter +'.4',
//            counter +'.5'
//        ] ).draw( false );
 
//        counter++;
//    } );
 
//    // Automatically add a first row of data
//    $('#addRow').click();
//} );
$(document).ready(function () {
    $("#mydatatable").DataTable({
        
        "ajax": {
            "url": "/Admin/GetCommunity",
            "type": "get",
            "datatype": "json"
            
        },
        "columns": [
                    { "data": "CommunityName", "autoWidth": true },
                    { "data": "CommunityDomain", "autoWidth": true },
                    { "data": "CommunityAbout", "autoWidth": true }
        ]



         
            });
    });
