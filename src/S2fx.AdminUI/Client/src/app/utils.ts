
/**
 * Block program for a moment
 * @param ms Time in milliseconds
 */
export async function wait(ms : number) : Promise<void>{
    return new Promise<void>((result, rej)=>{
        setTimeout(result, ms);
    });
}
