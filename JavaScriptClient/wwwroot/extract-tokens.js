let extractTokens = function (address) {

    let returnValue = address.split('#')[1];
    let values = returnValue.split('&');
    
    for (let i = 0; i < values.length; i++){
        let v = values[i];
        let kvp = v.split('=');
        localStorage.setItem(kvp[0], kvp[1]);
    }

    window.location.href = '/home/index';
};

extractTokens(window.location.href);