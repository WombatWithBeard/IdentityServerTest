let GetToken = function (address) {

    let returnValue = address.split('?')[1];
    let codeValue = returnValue.split('&')[0];
    let value = codeValue.split('=')[1];
    let url = "https://esia.gosuslugi.ru/aas/oauth2/te";
    
    let response = fetch(url + '?' + codeValue, {
        method: 'POST',
        headers: new Headers({
            "Accept": "*",
        }),
        mode: 'no-cors',
    }).then(resJson => {
        console.log(resJson)
    });
    
    let json = response.json();
    
    let smtth = "hkjlyul";
};

GetToken(window.location.href);