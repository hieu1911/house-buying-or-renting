import { useEventListener } from './event-listener';
import common from '../common/helper';

export function useOnClickOutside(rootEl, callback) {
    try {
        useEventListener(window, 'mouseup', (e) => {
            const clickedEl = e.target;
            
            if (rootEl.contains(clickedEl)) {
                return;
            }
    
            callback();
        });
    } catch (err) {
        common.handleError(err);
    }
}
