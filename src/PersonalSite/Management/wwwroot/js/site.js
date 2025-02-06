addEventListener("load", () => {
    document.querySelectorAll(".image-upload-area").forEach(el => {
        const relatedElement = document.querySelector(el.getAttribute("data-related-input"));
        el.addEventListener("click", () => {
            if (relatedElement) {
                relatedElement.click();
            }
            else {
                throw new Error("Cannot find related element");
            }
        });
        el.addEventListener("dragover", e => {
            e.preventDefault();
            e.stopPropagation();
        });
        el.addEventListener("drop", e => {
            e.preventDefault();
            e.stopPropagation();
            relatedElement.files = e.dataTransfer.files;
            relatedElement.dispatchEvent(new Event('change', { bubbles: true }));
            relatedElement.dispatchEvent(new Event('input', { bubbles: true }));
        });
    });
});