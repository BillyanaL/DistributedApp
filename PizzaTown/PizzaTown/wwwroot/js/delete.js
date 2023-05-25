$((function () {
    var url;
    var target;
    var redirectUrl;

    window.$('body').append(
        `
            <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel" style="color: grey !important;">Warning</h4>
                </div>
                <div class="modal-body delete-modal-body">
                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" id="cancel-delete">Cancel</button>
                    <button type="button" class="btn btn-danger" id="confirm-delete">Delete</button>
                </div>
                </div>
            </div>
            </div>
`);

    //Delete Action
    window.$("#deleteMealBtn").on('click',
        (e) => {
            e.preventDefault();

            target = e.target;
            var id = window.$(target).data('id');
            var controller = window.$(target).data('controller');
            var action = window.$(target).data('action');
            var bodyMessage = window.$(target).data('body-message');
            redirectUrl = $(target).data('redirect-url');

            url = "/" + controller + "/" + action + "/" + id;
            window.$(".delete-modal-body").text(bodyMessage);
            window.$("#deleteModal").modal('show');
        });

    window.$("#confirm-delete").on('click',
        () => {
            window.$.get(url)
                .done((result) => {
                    window.swal("Success", "Your pet has been removed!", "success");
                    
                })
                .fail((error) => {
                    swal("Error", "Your pet could not be removed!", "error");
                });

            window.$("#deleteModal").modal('hide');
            window.location.href = redirectUrl;
        });

    window.$("#cancel-delete").on('click', () => {
        window.$("#deleteModal").modal('hide');
    });

}()));