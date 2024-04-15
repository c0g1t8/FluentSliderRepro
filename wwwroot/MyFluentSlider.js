export function sliderUpdate(ref) {
    var fs = ref.getElementsByTagName('fluent-slider')[0];

    // fixes issue where you can't reset value programmatically
    fs.value = fs.attributes['value'].value;

    // fixes issue where the value indicator is not redrawn when either the min or max is changed
    fs.setThumbPositionForOrientation(fs.direction);
}
