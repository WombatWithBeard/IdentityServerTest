let createState = function () {
    return "SessionValueMakeItABitLonger";
};

let createNonce = function () {
    return "NonceValue";
};

let signIn = function () {
    let redirectUri = encodeURIComponent("https://localhost:5501/home/SignIn");
    let responseType = encodeURIComponent("id_token token");
    let scope = encodeURIComponent("openid ApiOne");

    let authUrl = '/connect/authorize/callback' +
        '?client_id=client_id_js' +
        '&redirect_uri=' + redirectUri +
        '&response_type=' + responseType +
        '&scope=' + scope +
        '&nonce=' + createNonce() +
        '&state=' + createState();

    let returnUrl = encodeURIComponent(authUrl);

    window.location.href = "https://localhost:4501/Auth/Login?ReturnUrl=" + returnUrl;
};