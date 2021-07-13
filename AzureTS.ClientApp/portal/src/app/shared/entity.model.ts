export class Entity{

    partitionKey: string | undefined;
    rowKey: string | undefined;
    name: string | undefined;
    lng: number = 0;
    lat: number = 0;
    head: number | undefined;
    speed: number | undefined;
    time: string | undefined;
    timestamp: Date | undefined

}