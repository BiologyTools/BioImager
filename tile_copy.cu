__global__ void copyTileToCanvas(
    unsigned char* canvas, int canvasWidth, int canvasHeight,
    unsigned char* tile, int tileWidth, int tileHeight,
    int offsetX, int offsetY,
    int canvasTileWidth, int canvasTileHeight)
{
    // Calculate the global x and y index for the thread
    int x = blockIdx.x * blockDim.x + threadIdx.x;
    int y = blockIdx.y * blockDim.y + threadIdx.y;

    // Check if the thread is within the bounds of the tile and canvas
    if (x < canvasTileWidth && y < canvasTileHeight) {
        int canvasX = x + offsetX;
        int canvasY = y + offsetY;

        // Ensure the canvas indices are within bounds
        if (canvasX < canvasWidth && canvasY < canvasHeight) {
            // Calculate corresponding tile indices, scaling tile pixels to canvas size
            float scaleX = (float)tileWidth / canvasTileWidth;
            float scaleY = (float)tileHeight / canvasTileHeight;

            // Calculate tile indices by scaling the x, y coordinates
            int tileX = (int)(x * scaleX);
            int tileY = (int)(y * scaleY);

            // Ensure tile indices are within the tile bounds
            if (tileX < tileWidth && tileY < tileHeight) {
                int tileIdx = (tileY * tileWidth + tileX) * 3;   // Each pixel has 3 components (RGB)
                int canvasIdx = (canvasY * canvasWidth + canvasX) * 3;

                // Copy the pixel (RGB components)
                canvas[canvasIdx] = tile[tileIdx];
                canvas[canvasIdx + 1] = tile[tileIdx + 1];
                canvas[canvasIdx + 2] = tile[tileIdx + 2];
            }
        }
    }
}
