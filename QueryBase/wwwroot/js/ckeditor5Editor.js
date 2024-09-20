import {
	ClassicEditor,
	AccessibilityHelp,
	Autoformat,
	AutoImage,
	Autosave,
	BlockQuote,
	Bold,
	CloudServices,
	Code,
	CodeBlock,
	Essentials,
	Heading,
	ImageBlock,
	ImageInsertViaUrl,
	ImageResizeEditing,
	ImageResizeHandles,
	ImageToolbar,
	ImageUpload,
	Indent,
	IndentBlock,
	Italic,
	Link,
	List,
	Paragraph,
	SimpleUploadAdapter,
	Table,
	TableCaption,
	TableCellProperties,
	TableColumnResize,
	TableProperties,
	TableToolbar,
	TextTransformation,
	Underline,
	Undo
} from 'ckeditor5';

const xsrfToken = document.querySelector('input[name="__RequestVerificationToken"]').value;

const editorConfig = {
	toolbar: {
		items: [
			'undo',
			'redo',
			'|',
			'heading',
			'|',
			'bold',
			'italic',
			'underline',
			'code',
			'|',
			'imageUpload',
			'link',
			'insertTable',
			'blockQuote',
			'codeBlock',
			'|',
			'bulletedList',
			'numberedList',
			'outdent',
			'indent',
			'|',
			'accessibilityHelp'
		],
		shouldNotGroupWhenFull: false
	},
	plugins: [
		AccessibilityHelp,
		Autoformat,
		AutoImage,
		Autosave,
		BlockQuote,
		Bold,
		CloudServices,
		Code,
		CodeBlock,
		Essentials,
		Heading,
		ImageBlock,
		ImageInsertViaUrl,
		ImageResizeEditing,
		ImageResizeHandles,
		ImageToolbar,
		ImageUpload,
		Indent,
		IndentBlock,
		Italic,
		Link,
		List,
		Paragraph,
		SimpleUploadAdapter,
		Table,
		TableCaption,
		TableCellProperties,
		TableColumnResize,
		TableProperties,
		TableToolbar,
		TextTransformation,
		Underline,
		Undo
	],
	heading: {
		options: [
			{
				model: 'paragraph',
				title: 'Paragraph',
				class: 'ck-heading_paragraph'
			},
			{
				model: 'heading1',
				view: 'h1',
				title: 'Heading 1',
				class: 'ck-heading_heading1'
			},
			{
				model: 'heading2',
				view: 'h2',
				title: 'Heading 2',
				class: 'ck-heading_heading2'
			},
			{
				model: 'heading3',
				view: 'h3',
				title: 'Heading 3',
				class: 'ck-heading_heading3'
			},
			{
				model: 'heading4',
				view: 'h4',
				title: 'Heading 4',
				class: 'ck-heading_heading4'
			},
			{
				model: 'heading5',
				view: 'h5',
				title: 'Heading 5',
				class: 'ck-heading_heading5'
			},
			{
				model: 'heading6',
				view: 'h6',
				title: 'Heading 6',
				class: 'ck-heading_heading6'
			}
		]
	},
	image: {
		toolbar: ['imageTextAlternative'],
		upload: {
			types:['png', 'jpeg', 'webp', 'tiff','bmp', 'svg']
		}
	},
	simpleUpload: {
		// The Url to where images are uploaded
		uploadUrl: window.location.href.slice(0, -3) + "UploadImage",

		// Enable the XMLHttpRequest.withCredentials property.
		/*withCredentials: true,*/

		//Headers sent along with the XMLHttpRequest to the upload server.
		//headers: {
		//	'X-CSRF-TOKEN': xsrfToken,
		//	/*Authorization: 'Bearer <JSON Web Token>'*/
		//}
	},
	initialData: "",
	link: {
		addTargetToExternalLinks: true,
		defaultProtocol: 'https://',
		decorators: {
			toggleDownloadable: {
				mode: 'manual',
				label: 'Downloadable',
				attributes: {
					download: 'file'
				}
			}
		}
	},
	table: {
		contentToolbar: ['tableColumn', 'tableRow', 'mergeTableCells', 'tableProperties', 'tableCellProperties']
	}
};




let unsavedUploadedImages = [];


window.addEventListener('beforeunload', () => {

	sendUnsavedImageList();

});


document.addEventListener('DOMContentLoaded', () => {
	document.querySelector('#discard').addEventListener('click', () => {
		sendUnsavedImageList();
	});
});

function sendUnsavedImageList(){
	if (unsavedUploadedImages.length > 0) {
		fetch('/Users/Question/DeleteUnsavedImages', {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({ 'unsavedImages': unsavedUploadedImages })
		})
	}
}



ClassicEditor.create(document.querySelector('#editor'), editorConfig).then((editor) => {


	function removeImageFromList(path) {
		let index = unsavedUploadedImages.indexOf(path);
		if (index !== -1) {
			unsavedUploadedImages.splice(index, 1);
			console.log('deleted');
		}
	}


	const observer = new MutationObserver((mutations) => {
		mutations.forEach((mutation) => {
			if (mutation.type === 'childList') {
				mutation.removedNodes.forEach((node) => {
					if (node.nodeName.toLowerCase() == 'figure' && node.classList.contains('image')) {

						let url = new URL(node.firstChild.src);
						removeImageFromList(url.pathname);
						console.log(unsavedUploadedImages);

						$.ajax({
							url: '/Users/Question/DeleteImage',
							type: 'POST',
							data: { imagePath: url.pathname },
							success: (res) => {
							},
							error: () => {
							}
						});
					}
				});



				mutation.addedNodes.forEach((node) => {
					if ((node.nodeName.toLowerCase() == 'figure' && node.classList.contains('image')) ||
						(node.nodeName.toLowerCase() == 'div' && node.classList.contains('ck-upload-placeholder-loader'))) {
						
						editor.plugins.get('FileRepository').loaders._items[0].on('change:uploadResponse', (e, n, v, o) => {
								if (v && unsavedUploadedImages.indexOf(v.url) === -1) {
									unsavedUploadedImages.push(v.url);
									console.log(unsavedUploadedImages);
								}
						});

					}
				});


			}
		});
	});

	// Configuration of the observer
	const config = { childList: true, subtree: true };

	// Start observing the document body
	document.addEventListener("DOMContentLoaded", () => {
		const targetNode = document.querySelector('.ck-content');
		if (targetNode) {
			observer.observe(targetNode, config);
		} else {
			console.error("Target element not found.");
		}
	});

});

