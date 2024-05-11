import { isRef, watch, onBeforeUnmount, unref } from "vue";
import common from '../common/helper';

export function useEventListener(target, event, handler) {
    try {
        if (isRef(target)) {
            watch(target, (value, oldValue) => {
                oldValue?.removeEventListener(event, handler);
                value?.addEventListener(event, handler);
            });
        } else {
            // Thên EventListener
            target.addEventListener(event, handler);
        }
    
        // Loại bỏ EventListener trước khi unmount
        onBeforeUnmount(() => {
            unref(target)?.removeEventListener(event, handler);
        });
    } catch (err) {
        common.handleError(err);
    }
}
  