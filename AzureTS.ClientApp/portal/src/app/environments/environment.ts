export const mapConstants =
{
  lat: 1.34577,
  long: 103.765,
  mapZoom: 11.5,
  style: 'mapbox://styles/mapbox/streets-v11',
  accessToken: 'pk.eyJ1Ijoic2ZtcyIsImEiOiJjand2czlxMzIwNGZsNDRwZGx2c3gwNWExIn0.aZXFKeOd42x8kkvl1udyKQ'
};

export class MapConstants {

  geoJsonObject = {
    'type': 'geojson',
    'data': {
      'type': 'Feature',
      'properties': {},
      'geometry': {
        'type': 'LineString',
        'coordinates': []
      }
    }
  }

  lineObject = {
    'id': 'route',
    'type': 'line',
    'source': 'route',
    'layout': {
      'line-join': 'round',
      'line-cap': 'round'
    },
    'paint': {
      'line-color': '#006400',
      'line-width': 2
    }
  }
}
