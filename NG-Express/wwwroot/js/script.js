function next(count)
{
    if (count <= 5) return;
    let cardBox = document.querySelector(".card-box");
    let marginleft = parseInt(window.getComputedStyle(cardBox).marginLeft);
    let width = parseInt(window.getComputedStyle(cardBox).width);
    console.log(width);
    if (marginleft > -width+1440)
    {
        if (width-1440  + marginleft < 800)
        {
            marginleft = -width+1440;

        } else
        {
        marginleft -= 800;
        }
        cardBox.style.marginLeft = `${marginleft}px`
    }
}
function prev(count) {
    if (count <= 5) return;
    let cardBox = document.querySelector(".card-box");
    let marginleft = parseInt(window.getComputedStyle(cardBox).marginLeft);
    if (marginleft < count*154) {
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