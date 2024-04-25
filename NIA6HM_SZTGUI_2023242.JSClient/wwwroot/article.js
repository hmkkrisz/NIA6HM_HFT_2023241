
let articles = [];
let connection = null;
let articleIdToUpdate = -1;
getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:58339/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ArticleCreated", (user, message) => {
        getdata();
    });

    connection.on("ArticleDeleted", (user, message) => {
        getdata();
    });
    connection.on("ArticleUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:58339/article')
        .then(x => x.json())
        .then(y => {
            articles = y;
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    articles.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.articleId + "</td><td>"
            + t.title + "</td><td>" +
            `<button type="button" onclick="remove(${t.articleId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.articleId})">Update</button>`
            + "</td></tr>";
    });
}
function showupdate(id) {
    document.getElementById('articlenametoupdate').value = articles.find(t => t['articleId'] == id)['title'];
    document.getElementById('updateformdiv').style.display = 'flex';
    articleIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:58339/article/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('articlename').value;
    fetch('http://localhost:58339/article/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { title: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('articlenametoupdate').value;
    fetch('http://localhost:58339/article/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { title: name, articleId: articleIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}
