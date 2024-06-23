function next()
{
    let cardBox = document.querySelector(".card-box");
    let marginleft = parseInt(window.getComputedStyle(cardBox).marginLeft);
    if (marginleft > -2310)
    {
        if (2310 + marginleft < 800)
        {
            marginleft = -2310;
        } else
        {
        marginleft -= 800;
        }
        cardBox.style.marginLeft = `${marginleft}px`
    }
}
function prev() {
    let cardBox = document.querySelector(".card-box");
    let marginleft = parseInt(window.getComputedStyle(cardBox).marginLeft);
    if (marginleft < 2310) {
        if (0-marginleft < 800) {
            marginleft = 0;
        }
        else
        {
        marginleft += 800;
        }
        cardBox.style.marginLeft = `${marginleft}px`
    }
}