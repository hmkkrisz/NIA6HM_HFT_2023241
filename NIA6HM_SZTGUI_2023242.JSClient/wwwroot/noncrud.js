
document.addEventListener('DOMContentLoaded', function () {
    const submitAuthStat = document.getElementById('submitAuthStat');
    const submitAvgLikes = document.getElementById('submitAvgLikes');
    const submitMostLiked = document.getElementById('submitMostLiked');
    const submitTop3 = document.getElementById('submitTop3');
    const submitArticleID = document.getElementById('submitArticleID');

    submitAuthStat.addEventListener('click', function () {
        getResult('Stat/AuthorStatistics', 'resultAuthStat');
    });

    submitAvgLikes.addEventListener('click', function () {    
        getResult('Stat/AvgLikesPerCategory', 'resultAvgLikes');
    });

    submitMostLiked.addEventListener('click', function () {
        getResult('Stat/GetMostLikedAuthor', 'resultMostLiked');
    });

    submitTop3.addEventListener('click', function () {
        getResult('Stat/Top3MostCommentedArticle', 'resultTop3');
    });

    submitArticleID.addEventListener('click', function () {
        const articleId = document.getElementById('inputArticleID').value;
        getResult('Stat/GetCommentsForArticle/' + articleId, 'resultArticleID');
    });

    function getResult(url, resultId) {
        fetch('http://localhost:58339/' + url)
            .then(response => response.text())
            .then(data => {
                const resultDiv = document.getElementById(resultId);
                resultDiv.innerHTML = `<p>${data}</p>`;
            })
            .catch(error => {
                console.error('Error:', error);
                const resultDiv = document.getElementById(resultId);
                resultDiv.innerHTML = '<p>An error occurred while fetching data</p>';
            });
    }
});
