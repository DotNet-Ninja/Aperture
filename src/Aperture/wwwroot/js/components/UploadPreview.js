class UploadPreview extends HTMLElement {
    constructor() {
        super();
        this.root = this.attachShadow({ mode: 'open' });
    }

    connectedCallback() {
        const className = this.getAttribute('className');
        const style = this.getAttribute('style');
        const stylesheet = this.getAttribute('stylesheet');
        const forControl = this.getAttribute('for');
        const control = document.getElementById(forControl);

        if (stylesheet && stylesheet.length) {
            const link = this.root.appendChild(document.createElement('link'));
            link.rel = 'stylesheet';
            link.href = stylesheet;
        }

        control.onchange = (event) => {
            for (var i = this.root.childNodes.length-1; i>=0; i--) {
                if (this.root.childNodes[i].tagName === 'IMG') {
                    this.root.removeChild(this.root.childNodes[i]);
                }
            }
            if (control.files && control.files.length > 0) {

                const img = document.createElement('img');
                img.className = className;
                img.style = style;
                this.root.appendChild(img);

                const reader = new FileReader();
                reader.onload = () => {
                    img.src = reader.result;
                    img.alt = control.files[0].name;
                };
                reader.readAsDataURL(control.files[0]);
            }
        };

    }
}

window.customElements.define('upload-preview', UploadPreview);