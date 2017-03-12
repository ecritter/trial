(function (document, window, undefined) {
    document.body.addEventListener("click", (e) => {
        if (e.target && e.target.matches("[data-action='like']")) {
            e.preventDefault();

            let dataItems = e.target.dataset.postId.split(',');

            var payloadJson = {};
            payloadJson["PostId"] = dataItems[0];
            payloadJson["UserEmail"] = dataItems[1];

            fetch(`/api/like/`, {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                method: "POST",
                body: JSON.stringify(payloadJson)
            })
                .then(response => {
                    if (response.status !== 200) {
                        throw new Error(response.statusText);
                    }

                    return response.json();
                })
                .then(likeViewModel => e.target.innerText = likeViewModel.Count);
        }
    });
})(document, window);