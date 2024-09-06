shader_type canvas_item;

uniform sampler2D screen_texture : hint_screen_texture, filter_linear_mipmap;

void fragment() {
	vec4 bg = texture(screen_texture, SCREEN_UV);
	float avg = (bg.r + bg.g + bg.b) / 3.f;
	// COLOR = vec4(avg, avg, avg, 1.f);
	COLOR = bg;
}
