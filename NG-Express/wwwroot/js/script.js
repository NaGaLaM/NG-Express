function next()
{
    let cardBox = document.querySelector(".card-box");
    let marginleft = parseInt(window.getComputedStyle(cardBox).marginLeft);
    if (marginleft > -2310)
    {
        if (2310 + marginleft < 770)
        {
            marginleft = -2310;
        } else
        {
        marginleft -= 770;
        }
        cardBox.style.marginLeft = `${marginleft}px`
    }
}
function prev() {
    let cardBox = document.querySelector(".card-box");
    let marginleft = parseInt(window.getComputedStyle(cardBox).marginLeft);
    if (marginleft < 2310) {
        if (0-marginleft < 770) {
            marginleft = 0;
        }
        else
        {
        marginleft += 770;
        }
        cardBox.style.marginLeft = `${marginleft}px`
    }
}